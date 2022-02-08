using MakanyApplication.Shared.Models.DataTransferObjects.Government;
using MakanyApplication.Shared.Models.ErrorHandler;
using MakanyApplication.Shared.Models.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakanyApplication.Shared.Models.Repository
{
    public class GovernmentRepo
    {
        private readonly MakanyApplicationDbContext _dbContext;

        public GovernmentRepo(MakanyApplicationDbContext db)
        {
            _dbContext = db;
        }

        public async Task<CommitResult<IndexGovernment>> GetDetailsAsync(int Id)
        {
            try
            {
                return new CommitResult<IndexGovernment>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<Government>().ProjectToType<IndexGovernment>().SingleOrDefaultAsync(government => government.Id.Equals(Id))
                };
            }
            catch
            {
                return new CommitResult<IndexGovernment>
                {
                    IsSuccess = false,
                    ErrorCode = "GM-X0001",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<IEnumerable<IndexGovernment>>> GetAsync()
        {
            try
            {
                return new CommitResult<IEnumerable<IndexGovernment>>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<Government>().Where(government => !government.IsDeleted).ProjectToType<IndexGovernment>().ToListAsync()
                };
            }
            catch
            {
                return new CommitResult<IEnumerable<IndexGovernment>>
                {
                    IsSuccess = false,
                    ErrorCode = "GM-X0001",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<UpdateGovernment>> GetGovernmentForUpdateAsync(int Id)
        {
            try
            {
                return new CommitResult<UpdateGovernment>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<Government>().ProjectToType<UpdateGovernment>().SingleOrDefaultAsync(government => government.Id.Equals(Id))
                };
            }
            catch
            {
                return new CommitResult<UpdateGovernment>
                {
                    IsSuccess = false,
                    ErrorCode = "GM-X0001",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<string>> CreateAsync(CreateGovernment model)
        {
            using (IDbContextTransaction tran = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    if (model is null)
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "GM-X0002",
                            ErrorType = ErrorType.Error
                        };
                    }

                    if (string.IsNullOrWhiteSpace(model.Name))
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "GM-X0003",
                            ErrorType = ErrorType.Error,
                            Value = default
                        };
                    }

                    if (string.IsNullOrWhiteSpace(model.Description))
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "GM-X0004",
                            ErrorType = ErrorType.Error,
                            Value = default
                        };
                    }

                    Government tempGovernment = await _dbContext.Set<Government>().Where(government => government.Name.Equals(model.Name) && government.Description.Equals(model.Description) && !government.IsDeleted).SingleOrDefaultAsync();

                    if (tempGovernment != null)
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "GM-X0005",
                            ErrorType = ErrorType.Error
                        };
                    }

                    tempGovernment = new Government
                    {
                        Name = model.Name,
                        Description = model.Description,
                        IsDeleted = false
                    };

                    _dbContext.Set<Government>().Add(tempGovernment);

                    await _dbContext.SaveChangesAsync();
                    await tran.CommitAsync();

                    return new CommitResult<string>
                    {
                        IsSuccess = true,
                        ErrorCode = string.Empty,
                        ErrorType = ErrorType.None,
                        Value = "Created Successfully"
                    };
                }
                catch
                {
                    await tran.RollbackAsync();
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "GM-X0001",
                        ErrorType = ErrorType.Error
                    };
                }
            }
        }

        public async Task<CommitResult<string>> UpdateAsync(UpdateGovernment model)
        {
            try
            {
                if (model is null)
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "GM-X0002",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                if (string.IsNullOrWhiteSpace(model.Name))
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "GM-X0003",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                if (string.IsNullOrWhiteSpace(model.Description))
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "GM-X0004",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                Government tempGovernment = await _dbContext.Set<Government>().Where(government => government.Name.Equals(model.Name) && government.Description.Equals(model.Description) && !government.IsDeleted).SingleOrDefaultAsync();

                if (tempGovernment != null)
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "GM-X0005",
                        ErrorType = ErrorType.Error
                    };
                }

                return new CommitResult<string>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = "Updated Successfully"
                };
            }
            catch
            {
                return new CommitResult<string>
                {
                    IsSuccess = false,
                    ErrorCode = "GM-X0001",
                    ErrorType = ErrorType.Error
                };
            }
        }
    }
}
