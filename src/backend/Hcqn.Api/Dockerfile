#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/backend/Hcqn.Api/Api.csproj", "src/backend/Hcqn.Api/"]
COPY ["src/backend/Core/Core.UnitOfWork/Core.UnitOfWork.csproj", "src/backend/Core/Core.UnitOfWork/"]
COPY ["src/backend/Core/Core.UnitOfWork.Abstractions/Core.UnitOfWork.Abstractions.csproj", "src/backend/Core/Core.UnitOfWork.Abstractions/"]
COPY ["src/backend/Hcqn.Infra.CrossCutting.Jwt/Hcqn.Infra.CrossCutting.Jwt.csproj", "src/backend/Hcqn.Infra.CrossCutting.Jwt/"]
COPY ["src/backend/Hcqn.Infra.CrossCutting.Identity/Hcqn.Infra.CrossCutting.Identity.csproj", "src/backend/Hcqn.Infra.CrossCutting.Identity/"]
COPY ["src/backend/Hcqn.Infra.CrossCutting.Identity.Configurations/Hcqn.Infra.CrossCutting.Identity.Configurations.csproj", "src/backend/Hcqn.Infra.CrossCutting.Identity.Configurations/"]
COPY ["src/backend/Hcqn.Caching/Hcqn.Caching.csproj", "src/backend/Hcqn.Caching/"]
COPY ["src/backend/Core/Core.Models/Core.Models.csproj", "src/backend/Core/Core.Models/"]
COPY ["src/backend/Core/Core.Notifications.Abstractions/Core.Notifications.Abstractions.csproj", "src/backend/Core/Core.Notifications.Abstractions/"]
COPY ["src/backend/Core/Core.Events.Abstractions/Core.Events.Abstractions.csproj", "src/backend/Core/Core.Events.Abstractions/"]
COPY ["src/backend/Core/Core.Tango/Core.Tango.csproj", "src/backend/Core/Core.Tango/"]
COPY ["src/backend/Core/Core.Identity.Abstractions/Core.Identity.Abstractions.csproj", "src/backend/Core/Core.Identity.Abstractions/"]
COPY ["src/backend/Core/Core.Bus.Abstractions/Core.Bus.Abstractions.csproj", "src/backend/Core/Core.Bus.Abstractions/"]
COPY ["src/backend/Core/Core.Commands/Core.Commands.csproj", "src/backend/Core/Core.Commands/"]
COPY ["src/backend/Core/Core.Types/Core.Types.csproj", "src/backend/Core/Core.Types/"]
COPY ["src/backend/Hcqn.AdministracaoUsuario.Application/Hcqn.AdministracaoUsuario.Application.csproj", "src/backend/Hcqn.AdministracaoUsuario.Application/"]
COPY ["src/backend/Hcqn.Domain/Hcqn.Domain.csproj", "src/backend/Hcqn.Domain/"]
COPY ["src/backend/Core/Core.Models.Extensions.NetTopologySuite/Core.Models.Extensions.NetTopologySuite.csproj", "src/backend/Core/Core.Models.Extensions.NetTopologySuite/"]
COPY ["src/backend/Hcqn.Infra.EntityFramework.InMemory/Hqn.Infra.EntityFramework.InMemory.csproj", "src/backend/Hcqn.Infra.EntityFramework.InMemory/"]
COPY ["src/backend/Core/Core.Notifications/Core.Notifications.csproj", "src/backend/Core/Core.Notifications/"]
COPY ["src/backend/Hcqn.AdministracaoPessoas.Application/Hcqn.AdministracaoPessoas.Application.csproj", "src/backend/Hcqn.AdministracaoPessoas.Application/"]
COPY ["src/backend/Core/Core.Models.Extensions.CustomValidators/Core.Models.Extensions.CustomValidators.csproj", "src/backend/Core/Core.Models.Extensions.CustomValidators/"]
COPY ["src/backend/Hcqn.AdministracaoPessoas.IoC/Hcqn.AdministracaoPessoas.IoC.csproj", "src/backend/Hcqn.AdministracaoPessoas.IoC/"]
COPY ["src/backend/Hcqn.AdministracaoPessoas.Infrastructure/Hcqn.AdministracaoPessoas.Infrastructure.csproj", "src/backend/Hcqn.AdministracaoPessoas.Infrastructure/"]
COPY ["src/backend/Hcqn.Infra.EntityFramework.PostgreSQL/Hcqn.Infra.EntityFramework.PostgreSQL.csproj", "src/backend/Hcqn.Infra.EntityFramework.PostgreSQL/"]
COPY ["src/backend/Hcqn.Infra.CrossCutting.EntityFramework.Extensions/Hcqn.Infra.EntityFramework.Extensions.csproj", "src/backend/Hcqn.Infra.CrossCutting.EntityFramework.Extensions/"]
COPY ["src/backend/Hcqn.EventSourcing.IoC/Hcqn.EventSourcing.IoC.csproj", "src/backend/Hcqn.EventSourcing.IoC/"]
COPY ["src/backend/Hcqn.EventSourcing.EntityFramework.PostgreSQL/Hcqn.EventSourcing.EntityFramework.PostgreSQL.csproj", "src/backend/Hcqn.EventSourcing.EntityFramework.PostgreSQL/"]
COPY ["src/backend/Hcqn.EventSourcing/Hcqn.EventSourcing.csproj", "src/backend/Hcqn.EventSourcing/"]
COPY ["src/backend/Core/Core.Bus/Core.Bus.csproj", "src/backend/Core/Core.Bus/"]
COPY ["src/backend/Hcqn.AdministracaoUsuario.IoC/Hcqn.AdministracaoUsuario.IoC.csproj", "src/backend/Hcqn.AdministracaoUsuario.IoC/"]
COPY ["src/backend/Hcqn.AdministracaoUsuario.Infrastructure/Hcqn.AdministracaoUsuario.Infrastructure.csproj", "src/backend/Hcqn.AdministracaoUsuario.Infrastructure/"]
COPY ["src/backend/Core/Core.Gis/Core.Gis.csproj", "src/backend/Core/Core.Gis/"]
RUN dotnet restore "src/backend/Hcqn.Api/Api.csproj"
COPY . .
WORKDIR "/src/src/backend/Hcqn.Api"
RUN dotnet build "Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]