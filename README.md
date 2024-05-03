# UK CoFID Nutritional Information Platform

This is a nutritional information platform that was created with data from the [Public Health England Composition of Foods Integrated Dataset](https://www.gov.uk/government/publications/composition-of-foods-integrated-dataset-cofid) (CoFID).

All of the Python code in the application ran using Python version 3.12.

The project contains three folders: data-cleaning, api, and streamlit-app.

## `data-cleaning`
This directory contains all the files and code for cleaning the .xlsx file with the CoFID dataset. If you would like to run the files locally, please install [PostgreSQL](https://www.postgresql.org/download/) for your machine. 

Follow these instructions to clean the dataset, create the required CSV and JSON files, and send the data to the local PostgreSQL database:
```powershell
cd nutrition-api-app/data-cleaning
```
```powershell
pip install -r requirements.txt
```
Ensure you have Jupyter Notebook installed and open the file `data_cleaning.ipynb` and run all the cells. Next run:

```powershell
python csv_to_json.py
```

Change the code in `data_to_sql.py` to your local PostgreSQL database details in order to correctly create the tables and populate the database.
```python
connection = psycopg2.connect(host='localhost', dbname='postgres',
                              user='postgres', password='12345')
```

If that is configured correctly, run:
```powershell
python data_to_sql.py
```

If the script ran successfully a success message should be printed and the tables and data should now be present in the database.

If none of the Python files run successfully, replace `python` with `python3` when running the scripts.


## `api`
This directory contains all the code for running the API.

To run this application:
```powershell
cd nutrition-api-app/api/NutritionAPI
```

```powershell
dotnet run
```

If it has compiled successfully, you will be presented with a `localhost` URL. To navigate to the Swagger UI section of the API you might need to append `/swagger/index.html` to the end of the of the URL.

If for any reason the project dependencies are not correctly installed or working, inside `/api/NutritionAPI` run:
```powershell
dotnet restore
```
```powershell
dotnet run
```

For ease of use and for marking purposes the required environment variables have been included in `launchSettings.json` in the directory `api/NutritionAPI/Properties`. For future development, this would have to be moved and more security measures would be introduced.

If you want to test the API connected to the local database, please change the connection string environment variable inside `launchSettings.json`. If not, it will be connected to the cloud-based database hosted by Neon.

Alternatively, the API can be accessed through the following URL which has been hosted on Azure [Azure Nutrition API](https://uol-nutrition-api.azurewebsites.net/swagger/index.html).

To obtain an API key, please use the POST endpoint `api/authentication` to register. Alternatively, a dummy user can be used which is already registered using the endpoint `api/authentication/login` with the following details:

```json
{
  "userName": "JBloggs",
  "password": "Password1000"
}
```

A token should then be returned which you can authorise through the Swagger UI or through `"Authorization": "Bearer <YOUR TOKEN>"` in the header of an API request.


## `streamlit-app`
This directory contains all the necessary code for running the Streamlit application to showcase how the API could be used in different ways.

To run this application:
```powershell
cd nutrition-api-app/streamlit-app
```

```powershell
pip install -r requirements.txt
```

```powershell
streamlit run Welcome.py
```

The application will be connected to the already cloud published API with Azure. If you wanted to test the API locally, the URL would have to be changed to the local URL running the API in `helper.py`.

Follow the instructions which are shown on the welcome page to obtain your API key so you can use the various features in the application.