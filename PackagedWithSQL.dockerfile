FROM ubuntu:20.04
ENV ASPNETCORE_ENVIRONMENT="Development"
ENV ACCEPT_EULA y
ENV SA_PASSWORD AtulVinod_@7
ENV ASPNETCORE_URLS=http://*:5000
RUN apt-get update
RUN apt-get install -y software-properties-common curl wget

# Adding MSSQL
RUN curl https://packages.microsoft.com/keys/microsoft.asc | tee /etc/apt/trusted.gpg.d/microsoft.asc
RUN curl https://packages.microsoft.com/config/ubuntu/20.04/prod.list | tee /etc/apt/sources.list.d/mssql-release.list
RUN add-apt-repository "$(wget -qO- https://packages.microsoft.com/config/ubuntu/20.04/mssql-server-2022.list)"
RUN apt-get update

#install dependencies
RUN apt-get install -y mssql-server mssql-tools18 unixodbc-dev nodejs npm dotnet-sdk-5.0
RUN dotnet tool install --global dotnet-ef --version 5.0.1
RUN mkdir source
COPY . /source
RUN chmod +x /source/entrypoint.sh

WORKDIR /source/CorporateQnA/UI
RUN npm i @angular/cli
RUN npm install
RUN node_modules/.bin/ng build --prod

WORKDIR /source
RUN dotnet publish -c Release -o out
WORKDIR /source/out
EXPOSE 5000
# CMD ASPNETCORE_URLS=http://*:$PORT dotnet CorporateQnA.dll
CMD ["/source/entrypoint.sh"]