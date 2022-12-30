using Autofac;
using Autofac.Extras.DynamicProxy;
using BusinessLayer.Abstract;
using BusinessLayer.Concreate.WorkFlow;
using CoreLayer.Utilities.Aspect;

using CoreLayer.Utilities.Result.Concreate;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concreate;
using EntityLayer.Models.EntityFremework;

namespace BusinessLayer.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Repository Dependency İnjection
            builder.RegisterType<MyBankContext>().InstancePerLifetimeScope();
            builder.RegisterType<AccountRepository>().As<IAccountRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>().InstancePerLifetimeScope();
            builder.RegisterType<BranchRepository>().As<IBranchRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AdressRespository>().As<IAdressRespository>().InstancePerLifetimeScope();
            builder.RegisterType<CountryRepository>().As<ICountryRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CityRepository>().As<ICityRepository>().InstancePerLifetimeScope();
            builder.RegisterType<DistrictRepository>().As<IDistrictRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AccountBalanceHistoryRepository>().As<IAccountBalanceHistoryRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MailRespository>().As<IMailRespository>().InstancePerLifetimeScope();
            builder.RegisterType<PhoneNumberRespository>().As<IPhoneNumberRespository>().InstancePerLifetimeScope();
            builder.RegisterType<CreditCardRespository>().As<ICreditCardRespository>().InstancePerLifetimeScope();
            builder.RegisterType<CardRespository>().As<ICardRespository>().InstancePerLifetimeScope();
            builder.RegisterType<AtmCardRespository>().As<IAtmCardRespository>().InstancePerLifetimeScope();
            builder.RegisterType<InternetPasswordRespository>().As<IInternetPasswordRespository>().InstancePerLifetimeScope();
            #endregion

            builder.Register(sp =>
            {
                #region constructor parameters
                var context = sp.Resolve<MyBankContext>();
                var accountRepo = sp.Resolve<IAccountRepository>();
                var customerRepo = sp.Resolve<ICustomerRepository>();
                var branchRepo = sp.Resolve<IBranchRepository>();
                var adressRespo = sp.Resolve<IAdressRespository>();
                var countryRepo = sp.Resolve<ICountryRepository>();
                var cityRepo = sp.Resolve<ICityRepository>();
                var districtRepo = sp.Resolve<IDistrictRepository>();
                var accountBalanceHistoryRepo = sp.Resolve<IAccountBalanceHistoryRepository>();
                var mailRespo = sp.Resolve<IMailRespository>();
                var phoneNumberRespo = sp.Resolve<IPhoneNumberRespository>();
                var creditCardRespo = sp.Resolve<ICreditCardRespository>();
                var cardRespo = sp.Resolve<ICardRespository>();
                var atmCardRepo = sp.Resolve<IAtmCardRespository>();
                var passwordRepo = sp.Resolve<IInternetPasswordRespository>();
                #endregion

                return new UnitOfWork(context, accountRepo, customerRepo, branchRepo, adressRespo, countryRepo, cityRepo, districtRepo, accountBalanceHistoryRepo, mailRespo, phoneNumberRespo, creditCardRespo, cardRespo, atmCardRepo, passwordRepo);
            }).As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<LogonWorkFlow>().As<ILogonWorkFlow>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerWorkFlow>().As<ICustomerWorkFlow>().InstancePerLifetimeScope();
            builder.RegisterType<AdressWorkFlow>().As<IAdressWorkFlow>().InstancePerLifetimeScope();
            builder.RegisterType<BranchWorkFlow>().As<IBranchWorkFlow>().InstancePerLifetimeScope();
            builder.RegisterType<EMailWorkFlow>().As<IEMailWorkFlow>().InstancePerLifetimeScope();
            builder.RegisterType<PhoneNumberWorkFlow>().As<IPhoneNumberWorkFlow>().InstancePerLifetimeScope();
            builder.RegisterType<CardWorkFlow>().As<ICardWorkFlow>().InstancePerLifetimeScope();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new Castle.DynamicProxy.ProxyGenerationOptions
                {
                    Selector = new AspectInterceptorSelector()
                }).InstancePerLifetimeScope();
        }
    }
}
