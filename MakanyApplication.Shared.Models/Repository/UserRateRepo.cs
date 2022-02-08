using MakanyApplication.Shared.Models.DataTransferObjects.UserRate;
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
    public class UserRateRepo
    {
        private readonly MakanyApplicationDbContext _dbContext;

        public UserRateRepo(MakanyApplicationDbContext db)
        {
            _dbContext = db;
        }

        public async Task<CommitResult<IndexUserRate>> GetDetailsAsync(int Id)
        {
            try
            {
                return new CommitResult<IndexUserRate>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<UserRate>().ProjectToType<IndexUserRate>().SingleOrDefaultAsync(userRate => userRate.Id.Equals(Id))
                };
            }
            catch
            {
                return new CommitResult<IndexUserRate>
                {
                    IsSuccess = false,
                    ErrorCode = "UR-X0001",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<IEnumerable<IndexUserRate>>> GetAsync()
        {
            try
            {
                return new CommitResult<IEnumerable<IndexUserRate>>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<UserRate>().Where(userRate => !userRate.IsDeleted).ProjectToType<IndexUserRate>().ToListAsync()
                };
            }
            catch
            {
                return new CommitResult<IEnumerable<IndexUserRate>>
                {
                    IsSuccess = false,
                    ErrorCode = "UR-X0001",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<UpdateUserRate>> GetUserRateForUpdateAsync(int Id)
        {
            try
            {
                return new CommitResult<UpdateUserRate>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<UserRate>().ProjectToType<UpdateUserRate>().SingleOrDefaultAsync(userRate => userRate.Id.Equals(Id))
                };
            }
            catch
            {
                return new CommitResult<UpdateUserRate>
                {
                    IsSuccess = false,
                    ErrorCode = "UR-X0001",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<string>> CreateAsync(CreateUserRate model)
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
                            ErrorCode = "UR-X0002",
                            ErrorType = ErrorType.Error
                        };
                    }

                    if (string.IsNullOrWhiteSpace(model.Comment))
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "UR-X0003",
                            ErrorType = ErrorType.Error,
                            Value = default
                        };
                    }

                    if (model.Value <= 0)
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "UR-X0004",
                            ErrorType = ErrorType.Error,
                            Value = default
                        };
                    }

                    if (model.ItemId <= 0)
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "UR-X0005",
                            ErrorType = ErrorType.Error,
                            Value = default
                        };
                    }

                    UserRate tempUserRate = await _dbContext.Set<UserRate>().Where(userRate => userRate.Comment.Equals(model.Comment) && !userRate.IsDeleted).SingleOrDefaultAsync();

                    if (tempUserRate != null)
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "UR-X0006",
                            ErrorType = ErrorType.Error
                        };
                    }

                    tempUserRate = new UserRate
                    {
                        Value = model.Value,
                        Comment = model.Comment,
                        ItemId = model.ItemId,
                        IsDeleted = false
                    };

                    _dbContext.Set<UserRate>().Add(tempUserRate);

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
                        ErrorCode = "UR-X0001",
                        ErrorType = ErrorType.Error
                    };
                }
            }
        }

        public async Task<CommitResult<string>> UpdateAsync(UpdateUserRate model)
        {
            try
            {
                if (model is null)
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "UR-X0002",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                if (string.IsNullOrWhiteSpace(model.Comment))
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "UR-X0003",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                if (model.Value <= 0)
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "UR-X0004",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                if (model.ItemId <= 0)
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "UR-X0005",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                UserRate tempUserRate = await _dbContext.Set<UserRate>().Where(userRate => userRate.Comment.Equals(model.Comment) && !userRate.IsDeleted).SingleOrDefaultAsync();

                if (tempUserRate != null)
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "UR-X0006",
                        ErrorType = ErrorType.Error
                    };
                }

                tempUserRate = new UserRate
                {
                    Value = model.Value,
                    Comment = model.Comment,
                    ItemId = model.ItemId,
                    IsDeleted = false
                };

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
                    ErrorCode = "UR-X0001",
                    ErrorType = ErrorType.Error
                };
            }
        }
    }
}
