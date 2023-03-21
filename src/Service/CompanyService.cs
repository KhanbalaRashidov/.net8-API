using Contracts;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace Service
{
    public class CompanyService : ICompanyService
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
            try
            {
                var companies = _repositoryManager.CompanyRepository.GetAllCompanies(trackChanges);

                var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);

                return companiesDto;
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Somethings went wrong in the {nameof(GetAllCompanies)} service method {ex}");
                throw;
            }
        }
    }
}
