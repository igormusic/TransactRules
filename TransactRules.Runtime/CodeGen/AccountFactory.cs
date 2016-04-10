using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransactRules.Configuration;
using System.Reflection;
using TransactRules.Runtime.Accounts;
using TransactRules.Core.Entities;

namespace TransactRules.Runtime.CodeGen
{
    public class AccountFactory
    {
        private static Dictionary<string, Type> types = new Dictionary<String, Type>();

        public static TransactionClient CreateTransactionClient(AccountType accountType, Account account)
        {
            var generatedType = GetGeneratedAccountType(accountType);

            TransactionClient client = (TransactionClient)Activator.CreateInstance(generatedType);

            client.Account = account;

            return client;
        }

        private static Type GetGeneratedAccountType(AccountType accountType)
        {
            var typeName = GetTypeName(accountType);

            Type type;

            lock (types)
            {
                if (types.ContainsKey(typeName) == false)
                {
                    type = GenerateAccountType(accountType);
                    types.Add(typeName, type);
                }
                else
                {
                    type = types[typeName];
                }
            }

            return type;
        }

        private static Type GenerateAccountType(AccountType accountType)
        {

            var provider = new Microsoft.CSharp.CSharpCodeProvider();
            var parameters = new System.CodeDom.Compiler.CompilerParameters { GenerateInMemory = true, GenerateExecutable = false };
            parameters.ReferencedAssemblies.Add("system.dll");
            parameters.ReferencedAssemblies.Add(typeof(System.Linq.IQueryable).Assembly.Location);
            parameters.ReferencedAssemblies.Add(typeof(Entity).Assembly.Location);
            parameters.ReferencedAssemblies.Add(typeof(AccountType).Assembly.Location);
            parameters.ReferencedAssemblies.Add(typeof(Account).Assembly.Location);
            parameters.ReferencedAssemblies.Add(typeof(System.ServiceModel.OperationContext).Assembly.Location);
            parameters.ReferencedAssemblies.Add(typeof(TransactRules.Calculations.AccrualCalculation).Assembly.Location);

            var accountTemplate = new AccountClassTemplate { Model = accountType };

            var code = accountTemplate.TransformText();

            var compilerResults = provider.CompileAssemblyFromSource(parameters, code);

            if (compilerResults.Errors.Count > 0)
            {
                ThrowCompilerException(accountType, compilerResults);
            }

            var generatedType =  compilerResults.CompiledAssembly.GetType(GetTypeName(accountType));
            return generatedType;
        }

        private static void ThrowCompilerException(AccountType accountType, System.CodeDom.Compiler.CompilerResults compilerResults)
        {
            var errors = new StringBuilder();

            errors.AppendLine("Following errors occured while compiling " + accountType.Name + ":");

            foreach (var error in compilerResults.Errors)
            {
                errors.AppendLine(error.ToString());
            }

            throw new ApplicationException(errors.ToString());
        }

        private static string GetTypeName(AccountType accountType)
        {
            return "TransactRules.Runtime." + accountType.Name;
        }
    }
}
