FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
COPY . .
RUN dotnet build -c Release -o /out

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS runtime
WORKDIR /app
COPY --from=build /out .
EXPOSE 6619
EXPOSE 6611
ENTRYPOINT ["dotnet", "LibraryApp.Presentation.dll"]