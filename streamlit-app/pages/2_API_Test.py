import api_handler
import credentials
import streamlit as st
import plotly.express as px
import pandas as pd

api_token = credentials.api_key 
# response = api_handler.get_data_from_api(api_token)

st.title("API Testing")

tab1, tab2 = st.tabs(["Macronutrient Distribution", "Specific Nutrient"])

with tab1:
    st.subheader("Every food item is displayed as per 100g")
    st.write("""
         ##### Search a food and see the macronutrient distribution
        """)
    food_to_search = st.text_input("Search for a food item", placeholder="Banana")
    response = api_handler.get_data_from_api(api_token, SearchFoodByName=food_to_search, PageSize=30)
    if food_to_search != "":
        for food_item in response.json():
            if st.button(food_item["Name"]):
                # st.write(f"Protein: {food_item['Macronutrients']['protein_g']}g")
                macronutrients = food_item['Macronutrients']
                fig = px.pie(names=['Protein', 'Fat', 'Carbohydrate'], values=list(macronutrients.values()), 
                            title=f"Macronutrient Values for {food_item['Name']}",
                            labels={"x": "Macronutrient", "y": "Grams"})
                fig.update_traces(textinfo='value')
                st.plotly_chart(fig)

                with st.expander("See more information"):
                    st.write(f"Food Code: {food_item['FoodCode']}")
                    st.write(f"Description of food: {food_item['Description']}")
                st.write(f"Data source: {food_item['DataReferences']}")
