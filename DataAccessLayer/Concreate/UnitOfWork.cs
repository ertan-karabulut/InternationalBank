using CoreLayer.DataAccess.Concrete;
using CoreLayer.DataAccess.Concrete.UnitOfWork;
using DataAccessLayer.Abstract;
using EntityLayer.Models.EntityFremework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concreate
{
    public class UnitOfWork : UnitOfWorkBase, IUnitOfWork
    {
        private readonly MyBankContext _context;
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IAdressRespository _adressRespository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IDistrictRepository _districtRepository;
        private readonly IAccountBalanceHistoryRepository _accountBalanceHistoryRepository;
        private readonly IMailRespository _mailRespository;
        private readonly IPhoneNumberRespository _phoneNumberRepository;
        private readonly ICreditCardRespository _creditCardRespository;
        private readonly ICardRespository _cardRespository;
        private readonly IAtmCardRespository _atmCardRespository;
        private readonly IInternetPasswordRespository _internetPasswordRespository;
        public UnitOfWork(MyBankContext context, IAccountRepository accountRepository, ICustomerRepository customerRepository, IBranchRepository branchRepository, IAdressRespository adressRespository, ICountryRepository countryRepository, ICityRepository cityRepository, IDistrictRepository districtRepository, IAccountBalanceHistoryRepository accountBalanceHistoryRepository, IMailRespository mailRespository, IPhoneNumberRespository phoneNumberRepository, ICreditCardRespository creditCardRespository, ICardRespository cardRespository, IAtmCardRespository atmCardRespository, IInternetPasswordRespository internetPasswordRespository) : base(context)
        {
            _context = context;
            this._accountRepository = accountRepository;
            this._customerRepository = customerRepository;
            this._branchRepository = branchRepository;
            this._countryRepository = countryRepository;
            this._cityRepository = cityRepository;
            this._districtRepository = districtRepository;
            this._accountBalanceHistoryRepository= accountBalanceHistoryRepository;
            this._mailRespository = mailRespository;
            this._phoneNumberRepository= phoneNumberRepository;
            this._creditCardRespository= creditCardRespository;
            this._cardRespository = cardRespository;
            this._atmCardRespository = atmCardRespository;
            this._adressRespository = adressRespository;
            this._internetPasswordRespository = internetPasswordRespository;
        }

        public IAccountRepository AccountRepository => this._accountRepository;

        public ICustomerRepository CustomerRepository => this._customerRepository;

        public IBranchRepository BranchRepository => this._branchRepository;

        public IAdressRespository AdressRespository => this._adressRespository;

        public ICountryRepository CountryRepository => this._countryRepository;

        public ICityRepository CityRepository => this._cityRepository;

        public IDistrictRepository DistrictRepository => this._districtRepository;
        public IAccountBalanceHistoryRepository AccountBalanceHistoryRepository => this._accountBalanceHistoryRepository;

        public IMailRespository MailRespository => this._mailRespository;

        public IPhoneNumberRespository PhoneNumberRepository => this._phoneNumberRepository;

        public ICreditCardRespository CreditCardRespository => this._creditCardRespository;

        public ICardRespository CardRespository => this._cardRespository;

        public IAtmCardRespository AtmCardRespository => this._atmCardRespository;

        public IInternetPasswordRespository InternetPasswordRespository => this._internetPasswordRespository;
    }
}
