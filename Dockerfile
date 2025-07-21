FROM mcr.microsoft.com/dotnet/sdk:8.0

WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . .

ENV ASPNETCORE_URLS=http://+:5000

EXPOSE 5000

CMD ["dotnet", "run"]
