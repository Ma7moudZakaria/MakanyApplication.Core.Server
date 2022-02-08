using MakanyApplication.Shared.Models.DataTransferObjects.Item;
using MakanyApplication.Shared.Models.ErrorHandler;
using MakanyApplication.Shared.Models.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakanyApplication.Shared.Models.Repository
{
    public class ItemRepo
    {
        private readonly MakanyApplicationDbContext _dbContext;

        public ItemRepo(MakanyApplicationDbContext db)
        {
            _dbContext = db;
        }

        public async Task<CommitResult<IndexItem>> GetDetailsAsync(int Id)
        {
            try
            {
                return new CommitResult<IndexItem>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<Item>().ProjectToType<IndexItem>().SingleOrDefaultAsync(item => item.Id.Equals(Id))
                };
            }
            catch
            {
                return new CommitResult<IndexItem>
                {
                    IsSuccess = false,
                    ErrorCode = "IT-X0001",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<IEnumerable<IndexItem>>> GetAsync()
        {
            try
            {
                return new CommitResult<IEnumerable<IndexItem>>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<Item>().Where(item => !item.IsDeleted).ProjectToType<IndexItem>().ToListAsync()
                };
            }
            catch
            {
                return new CommitResult<IEnumerable<IndexItem>>
                {
                    IsSuccess = false,
                    ErrorCode = "IT-X0001",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<UpdateItem>> GetItemForUpdateAsync(int Id)
        {
            try
            {
                return new CommitResult<UpdateItem>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<Item>().ProjectToType<UpdateItem>().SingleOrDefaultAsync(item => item.Id.Equals(Id))
                };
            }
            catch
            {
                return new CommitResult<UpdateItem>
                {
                    IsSuccess = false,
                    ErrorCode = "IT-X0001",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<string>> CreateAsync(CreateItem model)
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
                            ErrorCode = "IT-X0002",
                            ErrorType = ErrorType.Error
                        };
                    }

                    if (string.IsNullOrWhiteSpace(model.Title))
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "IT-X0003",
                            ErrorType = ErrorType.Error,
                            Value = default
                        };
                    }

                    if (string.IsNullOrWhiteSpace(model.Description))
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "IT-X0004",
                            ErrorType = ErrorType.Error,
                            Value = default
                        };
                    }

                    Item tempItem = await _dbContext.Set<Item>().Where(item => item.Title.Equals(model.Title) && item.Description.Equals(model.Description) && !item.IsDeleted).SingleOrDefaultAsync();

                    if (tempItem != null)
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "IT-X0005",
                            ErrorType = ErrorType.Error
                        };
                    }

                    tempItem = new Item
                    {
                        Title = model.Title,
                        Description = model.Description,
                        IsDeleted = false
                    };

                    _dbContext.Set<Item>().Add(tempItem);

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
                        ErrorCode = "IT-X0001",
                        ErrorType = ErrorType.Error
                    };
                }
            }
        }

        public async Task<CommitResult<string>> UpdateAsync(UpdateItem model)
        {
            try
            {
                if (model is null)
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "IT-X0002",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                if (string.IsNullOrWhiteSpace(model.Title))
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "IT-X0003",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                if (string.IsNullOrWhiteSpace(model.Description))
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "IT-X0004",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                Item tempItem = await _dbContext.Set<Item>().Where(item => item.Title.Equals(model.Title) && item.Description.Equals(model.Description) && !item.IsDeleted).SingleOrDefaultAsync();

                if (tempItem != null)
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "IT-X0005",
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
                    ErrorCode = "IT-X0001",
                    ErrorType = ErrorType.Error
                };
            }
        }
    }
}
