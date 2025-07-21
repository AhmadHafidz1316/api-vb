FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dev

WORKDIR /src
COPY . .

# Pastikan tool watch tersedia (biasanya sudah di SDK, tapi jaga-jaga)
RUN dotnet tool install --global dotnet-watch
ENV PATH="${PATH}:/root/.dotnet/tools"

# Masuk ke folder project .csproj kamu
WORKDIR /src/MyWebApi

# Jalankan dengan watch
CMD ["dotnet", "watch", "run", "--urls=http://0.0.0.0:5000"]
