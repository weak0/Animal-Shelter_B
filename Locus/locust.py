from locust import HttpUser, task, between, TaskSet

class UserBehavior(TaskSet):

    @task(1)
    def get_cost_categories(self):
        self.client.get("/costs/categories", verify=False)

    @task(1)
    def get_animal_sizes(self):
        self.client.get("/animals/sizes", verify=False)

    @task(1)
    def get_animal_types(self):
        self.client.get("/animals/types", verify=False)
    @task(1)
    def get_animals(self):
        self.client.get("/animals", verify=False)

    @task(1)
    def get_payemnt_period(self):
        self.client.get("/costs/paymentPeriods", verify=False)

    def get_animal_by_id(self):
        self.client.get("/animals/22", verify=False)

class WebsiteUser(HttpUser):
    tasks = [UserBehavior]
    wait_time = between(1, 5)