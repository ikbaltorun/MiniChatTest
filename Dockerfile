# Çalışma ortamı için hafif .NET 8 (veya kullandığın sürüm) imajı
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Kodu derlemek için SDK imajı
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MiniChatTest.csproj", "./"]
RUN dotnet restore "./MiniChatTest.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "MiniChatTest.csproj" -c Release -o /app/build

# Uygulamayı yayınlama (Publish)
FROM build AS publish
RUN dotnet publish "MiniChatTest.csproj" -c Release -o /app/publish

# Son aşama: Çalıştırılabilir dosyayı base imaja kopyala
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MiniChatTest.dll"]