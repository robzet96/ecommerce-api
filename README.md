CICD
1. Stworzenie Azure Web App
2. Pobranie publish profile i dodanie do sekretów repozytorium github secrets settings > secrets and variables > actions > repository secrets
3. W repozytorium utworzenie .github/workflows i umieszczenie tam pliku, zawierającego informacje odnośnie build i deploy
4. Na portalu azure zamieszczenie informacji potrzebnych do prawidłowego działania aplikacji, które pierwotnie były w appsettings.json. Mianowicie klucza do JWT, JWT_Issuer a także connectionstring do DB
5. W trakcie tworzenia tego procesu musiałem również "|| app.Environment.IsProduction()" w program.cs by móc korzystać ze swaggera
6. Po doknaniu powyższych push do github uruchamia workflow
7. W ramach testu dodałem również dodatkowy endpoint "helloworld"

Połączenie z API
Api dostępne jest pod linkiem https://robzet-ecommerce-api-cfaje8ajauddh7hq.westeurope-01.azurewebsites.net/swagger/index.html
Wykorzystane usługi to :
 1. Azure Database for PostgreSQL
 2. Azure App Service
