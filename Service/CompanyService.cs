using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;

        public CompanyService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }

        public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
        {
            var companies = _repositoryManager.CompanyRepository.GetAllCompanies(trackChanges);

            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);

            return companiesDto;
        }

        public CompanyDto GetCompany(Guid companyId, bool trackChanges)
        {
            var company = _repositoryManager.CompanyRepository.GetCompany(companyId, trackChanges);

            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var companyDto = _mapper.Map<CompanyDto>(company);

            return companyDto;
        }

        public CompanyDto CreateCompany(CompanyForCreationDto company)
        {
            var companyEntity = _mapper.Map<Company>(company);

            _repositoryManager.CompanyRepository.CreateCompany(companyEntity);
            _repositoryManager.Save();

            var companyToReturn = _mapper.Map<CompanyDto>(companyEntity);

            return companyToReturn;
        }

        public IEnumerable<CompanyDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();

            var companyEntities = _repositoryManager.CompanyRepository.GetByIds(ids, trackChanges);
            if (ids.Count() != companyEntities.Count())
                throw new CollectionByIdsBadRequestException();

            var companiesToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);

            return companiesToReturn;
        }

        public (IEnumerable<CompanyDto> companies, string ids) CreateCompanyCollection(IEnumerable<CompanyForCreationDto> companyCollection)
        {
            if (companyCollection is null)
                throw new CompanyCollectionBadRequest();

            var companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollection);
            foreach (var company in companyEntities)
            {
                _repositoryManager.CompanyRepository.CreateCompany(company);
            }

            _repositoryManager.Save();

            var companyCollectionToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
            var ids = string.Join(",", companyCollectionToReturn.Select(c => c.Id));

            return (companies: companyCollectionToReturn, ids: ids);
        }

        public void DeleteCompany(Guid companyId, bool trackChanges)
        {
            var company = _repositoryManager.CompanyRepository.GetCompany(companyId, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);

            _repositoryManager.CompanyRepository.DeleteCompany(company);
            _repositoryManager.Save();
        }

        public void UpdateCompany(Guid companyId, CompanyForUpdateDto companyForUpdate, bool trackChanges)
        {
            var companyEntity = _repositoryManager.CompanyRepository.GetCompany(companyId, trackChanges);
            if (companyEntity is null)
                throw new CompanyNotFoundException(companyId);

            _mapper.Map(companyForUpdate, companyEntity);
            _repositoryManager.Save();
        }
    }
}
