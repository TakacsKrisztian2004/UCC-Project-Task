﻿namespace backend.Models.Dtos
{
    public record UserDto(Guid Id, string? Name, ICollection<Report> Reports);
    public record CreateUserDto(string? Name);
    public record RemoveUserDto(Guid Id);
    public record ModifyUserDto(string? Name);
}