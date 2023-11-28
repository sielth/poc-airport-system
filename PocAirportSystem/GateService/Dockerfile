FROM mcr.microsoft.com/dotnet/aspnet:7.0

EXPOSE 80
EXPOSE 443

WORKDIR /app

COPY bin/Debug/net7.0 .

ENTRYPOINT ["dotnet", "GateService.dll"]