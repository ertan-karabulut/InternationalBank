using CoreLayer.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IUnitOfWork : IUnitOfWorkBase
    {
        IAccountRepository AccountRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IBranchRepository BranchRepository { get; }
        IAdressRespository AdressRespository { get; }
        ICountryRepository CountryRepository { get; }
        ICityRepository CityRepository { get; }
        IDistrictRepository DistrictRepository { get; }
        IAccountBalanceHistoryRepository AccountBalanceHistoryRepository { get; }
        IMailRespository MailRespository { get; }
        IPhoneNumberRespository PhoneNumberRepository { get; }
        ICreditCardRespository CreditCardRespository { get; }
        ICardRespository CardRespository { get; }
        IAtmCardRespository AtmCardRespository { get; }
        IInternetPasswordRespository InternetPasswordRespository { get; }
    }
}
