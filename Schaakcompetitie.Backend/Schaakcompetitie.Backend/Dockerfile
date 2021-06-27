FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
ARG source
WORKDIR /app
COPY ${source:-./obj/Docker/publish} .
EXPOSE 80 443
ENTRYPOINT ["dotnet", "Schaakcompetitie.Backend.dll"]