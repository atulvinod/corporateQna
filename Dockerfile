FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
ENV ASPNETCORE_ENVIRONMENT="Development"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/source/https/aspnetapp.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=atulvinod
RUN apt update
RUN apt install nodejs npm -y
RUN mkdir source
COPY . /source

WORKDIR /source/CorporateQnA/UI
RUN npm i @angular/cli
RUN npm install
RUN node_modules/.bin/ng build --prod

WORKDIR /source
RUN dotnet publish -c Release -o out
WORKDIR /source/out
CMD ASPNETCORE_URLS=https://*:$PORT dotnet CorporateQnA.dll
