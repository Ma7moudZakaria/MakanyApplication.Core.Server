using MakanyApplication.Shared.Models.DataTransferObjects.PhoneNumber;
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
    public class PhoneNumberRepo
    {
        private readonly MakanyApplicationDbContext _dbContext;

        public PhoneNumberRepo(MakanyApplicationDbContext db)
        {
            _dbContext = db;
        }

        public async Task<CommitResult<IndexPhoneNumber>> GetDetailsAsync(int Id)
        {
            try
            {
                return new CommitResult<IndexPhoneNumber>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<PhoneNumber>().ProjectToType<IndexPhoneNumber>().SingleOrDefaultAsync(phoneNumber => phoneNumber.Id.Equals(Id))
                };
            }
            catch
            {
                return new CommitResult<IndexPhoneNumber>
                {
                    IsSuccess = false,
                    ErrorCode = "PN-X0001",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<IEnumerable<IndexPhoneNumber>>> GetAsync()
        {
            try
            {
                return new CommitResult<IEnumerable<IndexPhoneNumber>>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<PhoneNumber>().Where(phoneNumber => !phoneNumber.IsDeleted).ProjectToType<IndexPhoneNumber>().ToListAsync()
                };
            }
            catch
            {
                return new CommitResult<IEnumerable<IndexPhoneNumber>>
                {
                    IsSuccess = false,
                    ErrorCode = "PN-X0001",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<UpdatePhoneNumber>> GetPhoneNumberForUpdateAsync(int Id)
        {
            try
            {
                return new CommitResult<UpdatePhoneNumber>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<PhoneNumber>().ProjectToType<UpdatePhoneNumber>().SingleOrDefaultAsync(phoneNumber => phoneNumber.Id.Equals(Id))
                };
            }
            catch
            {
                return new CommitResult<UpdatePhoneNumber>
                {
                    IsSuccess = false,
                    ErrorCode = "PN-X0001",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<string>> CreateAsync(CreatePhoneNumber model)
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
                            ErrorCode = "PN-X0002",
                            ErrorType = ErrorType.Error
                        };
                    }

                    if (string.IsNullOrWhiteSpace(model.Phone))
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "PN-X0003",
                            ErrorType = ErrorType.Error,
                            Value = default
                        };
                    }

                    if (model.ItemId <= 0)
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "PN-X0004",
                            ErrorType = ErrorType.Error,
                            Value = default
                        };
                    }

                    if (model.UserId <= 0)
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "PN-X0005",
                            ErrorType = ErrorType.Error,
                            Value = default
                        };
                    }

                    PhoneNumber tempPhoneNumber = await _dbContext.Set<PhoneNumber>().Where(phoneNumber => phoneNumber.Phone.Equals(model.Phone) && !phoneNumber.IsDeleted).SingleOrDefaultAsync();

                    if (tempPhoneNumber != null)
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "PN-X0005",
                            ErrorType = ErrorType.Error
                        };
                    }

                    tempPhoneNumber = new PhoneNumber
                    {
                        Phone = model.Phone,
                        ItemId = model.ItemId,
                        UserId = model.UserId,
                        IsDeleted = false
                    };

                    _dbContext.Set<PhoneNumber>().Add(tempPhoneNumber);

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
                        ErrorCode = "PN-X0001",
                        ErrorType = ErrorType.Error
                    };
                }
            }
        }

        public async Task<CommitResult<string>> UpdateAsync(UpdatePhoneNumber model)
        {
            try
            {
                if (model is null)
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "PN-X0002",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                if (model.ItemId <= 0)
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "PN-X0004",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                if (model.UserId <= 0)
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "PN-X0005",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                PhoneNumber tempPhoneNumber = await _dbContext.Set<PhoneNumber>().Where(phoneNumber => phoneNumber.Phone.Equals(model.Phone) && !phoneNumber.IsDeleted).SingleOrDefaultAsync();

                if (tempPhoneNumber != null)
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "PN-X0005",
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
                    ErrorCode = "PN-X0001",
                    ErrorType = ErrorType.Error
                };
            }
        }
    }
}
