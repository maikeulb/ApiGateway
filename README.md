# Github API Gateway

API Gateway for Github powered by Reactive Extensions (Rx). Built following
this [blog
post](http://blog.tamizhvendan.in/blog/2015/12/29/implementing-api-gateway-in-f-number-using-rx-and-suave/).

Facade API hiding the complexity for calling multiple
services by exposing a single endpoint. The exposed API retrieves 3 components:
User, Popular Repos, and Languages.

Technology
----------
* Suave
* Rx

Endpoints
---------

| Method     | URI                                  | Action                                      |
|------------|--------------------------------------|---------------------------------------------|
| `GET`      | `/api/profile/{username}`            | `User profile information`                  |

Sample Usage
---------------

`http get http://localhost:8080/api/profiles/miguelgrinberg` (my favorite python/flask instructor)
```
    "avatarUrl": "https://avatars0.githubusercontent.com/u/2715854?v=4", 
    "name": "Miguel Grinberg", 
    "popularRepositories": [
        {
            "languages": [
                "Python"
            ], 
            "name": "Flask-HTTPAuth", 
            "stars": 453
        }, 
        {
            "languages": [
                "Python", 
                "JavaScript", 
                "CSS", 
                "HTML", 
                "Shell"
            ], 
            "name": "flack", 
            "stars": 294
        }, 
        {
            "languages": [
                "Python", 
                "HTML", 
                "Shell"
            ], 
            "name": "flask-celery-example", 
            "stars": 396
        }
    ]
}
```

Run
---

You need Mono, forge, and fake.

```
forge fake run
Go to http://localhost:8080 and visit the above endpoint
```

TODO
----
Dockerfile
