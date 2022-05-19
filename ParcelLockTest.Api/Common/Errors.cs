using System.Net;

namespace ParcelLockTest.Api.Common;

public static class Errors {
    public static class Order {
        public static HttpResponseException NotFound(int id) => new($"Заказ {id} не найден") { ErrorCode = 101, StatusCode = HttpStatusCode.NotFound };
        public static HttpResponseException ParcelLockNotFound(string number) => new($"Не удалось создать заказ, замок не найден") { ErrorCode = 102, StatusCode = HttpStatusCode.BadRequest };

        // странно, что здесь Forbidden по требованиям, хотя он тут по смыслу не подходит
        public static HttpResponseException ParcelLockNotActive(string number) => new($"Не удалось создать заказ, замок не активен") { ErrorCode = 103, StatusCode = HttpStatusCode.Forbidden };
    }
}
