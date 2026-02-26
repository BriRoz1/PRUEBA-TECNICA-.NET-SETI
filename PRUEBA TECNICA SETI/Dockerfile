
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src


COPY ["PRUEBA TECNICA SETI.csproj", "./"]
RUN dotnet restore "PRUEBA TECNICA SETI.csproj"


COPY . .


WORKDIR "/src"
RUN dotnet build "PRUEBA TECNICA SETI.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN dotnet publish "PRUEBA TECNICA SETI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
USER app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "PRUEBA TECNICA SETI.dll"]
