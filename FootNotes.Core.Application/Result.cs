using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootNotes.Core.Application
{
    public class Result<T>
    {
        public bool Successed { get; }
        public string? Error { get; }
        public T? Value { get; }

        private Result(bool isSuccess, T? value, string? error)
        {
            Successed = isSuccess;
            Value = value;
            Error = error;
        }

        public static Result<T> Success(T value) => new(true, value, null);
        public static Result<T> Failure(string error) => new(false, default, error);
        public static Result<T> Failure(string error, T value) => new(false, value, error);
    }
}
