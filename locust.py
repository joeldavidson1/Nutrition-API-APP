from locust import HttpUser, task, between, LoadTestShape
import random


# STRESS TEST
# class CustomLoadTestShape(LoadTestShape):
#     """
#     A custom load test shape that ramps up to 500 users over the first 180 seconds, maintains 500 users for 600 seconds (10 minutes),
#     and then ramps down to 0 users over the last 180 seconds.
#     """
    
#     def tick(self):
#         run_time = self.get_run_time()
        
#         # Ramp up to 500 users over the first 180 seconds
#         if run_time < 180:
#             user_count = round((run_time / 180) * 500)
#             return (user_count, user_count)

#         # Maintain 500 users for the next 600 seconds (10 minutes)
#         elif run_time < 780:  # 180 seconds + 600 seconds = 780 seconds
#             return (500, 500)

#         # Ramp down to 0 users over the last 180 seconds
#         elif run_time < 960:  # 10 minutes + 180 seconds = 960 seconds
#             remaining_time = 960 - run_time
#             user_count = round(500 * (remaining_time / 180))
#             return (user_count, user_count)

#         # Test is complete, no more users
#         else:
#             return None


# SPIKE TEST
# class CustomLoadTestShape(LoadTestShape):
#     """
#     A custom load test shape that ramps up to 2000 users over 2 minutes and ramps down to 0 users over 1 minute.
#     """
    

#     def tick(self):
#         run_time = self.get_run_time()
#         users = 1500
        
#         # Ramp up to 1500 users over the first 120 seconds (2 minutes)
#         if run_time < 120:
#             user_count = round((run_time / 120) * users)
#             return (user_count, user_count)

#         # Ramp down to 0 users over the next 60 seconds (1 minute)
#         elif run_time < 180:
#             remaining_time = 180 - run_time
#             user_count = round(users * (remaining_time / 60))
#             return (user_count, user_count)

#         # Test is complete, no more users
#         else:
#             return None


# AVERAGE LOAD TEST - TYPICAL DAY
class CustomLoadTestShape(LoadTestShape):
    # """
    # A custom load test shape that ramps up to 200 users over the first 180 seconds, maintains 200 users for 600 seconds (10 minutes),
    # and then ramps down to 0 users over the last 180 seconds.
    # """
    
    def tick(self):
        run_time = self.get_run_time()
        users = 200
        
        # Ramp up to 200 users over the first 180 seconds
        if run_time < 180:
            user_count = round((run_time / 180) * users)
            return (user_count, user_count)

        # Maintain 200 users for the next 600 seconds (10 minutes)
        elif run_time < 780:  # 180 seconds + 600 seconds = 780 seconds
            return (users, users)

        # Ramp down to 0 users over the last 180 seconds
        elif run_time < 960:  # 10 minutes + 180 seconds = 960 seconds
            remaining_time = 960 - run_time
            user_count = round(users * (remaining_time / 180))
            return (user_count, user_count)

        # Test is complete, no more users
        else:
            return None


class QuickstartUser(HttpUser):    
    def __init__(self, parent):
        super(QuickstartUser, self).__init__(parent)
        self.token = ""
        self.headers = {}

    wait_time = between(1, 1)

    def on_start(self):
        self.client.verify = False

        payload = {
            "userName": "JBloggs",
            "password": "Password1000"
        }
        
        response = self.client.post("/api/authentication/login", json=payload)
        # print(f"RESPONSE: {response.text}")
        self.token = response.json()["token"]
        self.headers = {"Authorization": f"Bearer {self.token}"}

    @task
    def view_food_by_id(self):
        ids = ["17-705", "17-769", "14-804", "14-805", "17-844", "13-077", 
               "13-076", "13-662","13-074","15-705", "13-190", "13-584"]


        random_id = random.choice(ids)
        # self.client.verify = False
        self.client.get(f"/api/foodItems/{random_id}", headers=self.headers)
        
    @task
    def view_food(self):
        self.client.get("/api/foodItems", headers=self.headers)
        
    @task
    def view_food_by_food_group(self):
        ids = ["JM", "AS", "AT", "JK", "C", "DG", "OA", "OB"]
        random_id = random.choice(ids)
        
        self.client.get(f"/api/foodItems/{random_id}/foodItems", headers=self.headers)
        
    @task
    def view_food_items_with_search_parameter(self):
        searches = ["cheese", "banana", "egg", "apple", "pork"]
        random_search = random.choice(searches)
        
        self.client.get(f"/api/foodItems?SearchFoodByName={random_search}", headers=self.headers)
        
    @task
    def view_food_items_with_ordering_parameter(self):
        orderings = ["macronutrients protein_g desc", "vitamins retinol_mcg desc", "minerals calcium_mg desc"]
        random_order = random.choice(orderings)
        
        self.client.get(f"/api/foodItems?OrderBy={random_order}", headers=self.headers)
  