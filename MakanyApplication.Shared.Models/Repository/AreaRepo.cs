using MakanyApplication.Shared.Models.DataTransferObjects.Area;
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
    public class AreaRepo
    {
        private readonly MakanyApplicationDbContext _dbContext;

        public AreaRepo(MakanyApplicationDbContext db)
        {
            _dbContext = db;
        }

        public async Task<CommitResult<IndexArea>> GetDetailsAsync(int Id)
        {
            try
            {
                return new CommitResult<IndexArea>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<Area>().ProjectToType<IndexArea>().SingleOrDefaultAsync(area => area.Id.Equals(Id))
                };
            }
            catch
            {
                return new CommitResult<IndexArea>
                {
                    IsSuccess = false,
                    ErrorCode = "AR-X0001",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<IEnumerable<IndexArea>>> GetAsync()
        {
            try
            {
                return new CommitResult<IEnumerable<IndexArea>>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<Address>().Where(area => !area.IsDeleted).ProjectToType<IndexArea>().ToListAsync()
                };
            }
            catch
            {
                return new CommitResult<IEnumerable<IndexArea>>
                {
                    IsSuccess = false,
                    ErrorCode = "AR-X0001",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<UpdateArea>> GetAreaForUpdateAsync(int Id)
        {
            try
            {
                return new CommitResult<UpdateArea>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<Address>().ProjectToType<UpdateArea>().SingleOrDefaultAsync(area => area.Id.Equals(Id))
                };
            }
            catch
            {
                return new CommitResult<UpdateArea>
                {
                    IsSuccess = false,
                    ErrorCode = "AR-X0001",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<string>> CreateAsync(CreateArea model)
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
                            ErrorCode = "AR-X0002",
                            ErrorType = ErrorType.Error
                        };
                    }

                    if (string.IsNullOrWhiteSpace(model.Name))
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "AR-X0003",
                            ErrorType = ErrorType.Error,
                            Value = default
                        };
                    }

                    if (string.IsNullOrWhiteSpace(model.Description))
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "AR-X0004",
                            ErrorType = ErrorType.Error,
                            Value = default
                        };
                    }

                    Area tempArea = await _dbContext.Set<Area>().Where(area => area.Name.Equals(model.Name) && area.Description.Equals(model.Description) && !area.IsDeleted).SingleOrDefaultAsync();

                    if (tempArea != null)
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "AR-X0005",
                            ErrorType = ErrorType.Error
                        };
                    }

                    tempArea = new Area
                    {
                        Name = model.Name , Description = model.Description , IsDeleted = false
                    };

                    _dbContext.Set<Area>().Add(tempArea);

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
                        ErrorCode = "AR-X0001",
                        ErrorType = ErrorType.Error
                    };
                }
            }
        }

        public async Task<CommitResult<string>> UpdateAsync(UpdateArea model)
        {
            try
            {
                if (model is null)
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "AR-X0002",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                if (string.IsNullOrWhiteSpace(model.Name))
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "AR-X0003",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                if (string.IsNullOrWhiteSpace(model.Description))
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "AR-X0004",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                Area tempArea = await _dbContext.Set<Area>().Where(area => area.Name.Equals(model.Name) && area.Description.Equals(model.Description) && !area.IsDeleted).SingleOrDefaultAsync();

                if (tempArea != null)
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "AR-X0005",
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
                    ErrorCode = "AR-X0001",
                    ErrorType = ErrorType.Error
                };
            }
        }
    }
}
