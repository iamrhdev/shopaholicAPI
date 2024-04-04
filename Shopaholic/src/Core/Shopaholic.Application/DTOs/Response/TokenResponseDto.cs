namespace Shopaholic.Application.DTOs.Response
{
    public record TokenResponseDto(string jwt,
                                   DateTime jwtExpiration,
                                   string refreshToken,
                                   DateTime refreshTokenExpiration);
}
