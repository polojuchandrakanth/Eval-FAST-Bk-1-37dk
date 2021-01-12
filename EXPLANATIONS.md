# Technical EXPLANATIONS

## Objective
--> When ever we're trying to compose a project to docker we're getting issue of "permissionerror-errno-13-permission-denied-v15-server-sqlite3-dbLock", then we've added ".dockerignore" 
	file for over coming this issue.
--> Missed launchSettings.josn file added it.

### API EXPLANATIONS
{
  "FORTAPI": "1.0.0",
  "info": {
    "title": "FORT Service API",
    "description": "This is Fort API Task where user can register, add Favourite Country & City, Retervie there Country & City & Deleteting the Country.",
    "version": "v1.0.0"
  },
  "paths": {
    "/account/adduser": {
      "post": {
        "tags": [
          "Account"
        ],
        "summary": "Adding a new users",
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/AddUserRequest"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/AddUserRequest"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/AddUserRequest"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success",
            "content": {
              "text/plain": {
                "schema": {
                  "type": "string"
                }
              },
              "application/json": {
                "schema": {
                  "type": "string"
                }
              },
              "text/json": {
                "schema": {
                  "type": "string"
                }
              }
            }
          },
          "422": {
            "description": "User Creation Failed"
          },
          "500": {
            "description": "Exception Message"
          }
        }
      }
    },
    "/account/addcountry": {
      "post": {
        "tags": [
          "Account"
        ],
        "requestBody": {
          "content": {
            "application/json": {
              "schema": {
                "$ref": "#/components/schemas/AddCountryRequest"
              }
            },
            "text/json": {
              "schema": {
                "$ref": "#/components/schemas/AddCountryRequest"
              }
            },
            "application/*+json": {
              "schema": {
                "$ref": "#/components/schemas/AddCountryRequest"
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "Success",
            "content": {
              "text/plain": {
                "schema": {
                  "type": "string"
                }
              },
              "application/json": {
                "schema": {
                  "type": "string"
                }
              },
              "text/json": {
                "schema": {
                  "type": "string"
                }
              }
            }
          }
        }
      }
    },
    "/account/getcountry": {
      "get": {
        "tags": [
          "Account"
        ],
        "summary": "Retervie favorite Countrys and Citys",
        "description": "we are fetching the data based on the userid, if the userid is null or zero then we're sending badrequest as Username or password is incorrect",
        "responses": {
          "200": {
            "description": "Success",
            "content": {
              "text/plain": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/User"
                  }
                }
              },
              "application/json": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/User"
                  }
                }
              },
              "text/json": {
                "schema": {
                  "type": "array",
                  "items": {
                    "$ref": "#/components/schemas/User"
                  }
                }
              }
            }
          },
          "500": {
            "description": "Exception Message"
          }
        }
      }
    }
  },
  "components": {
    "schemas": {
      "AddUserRequest": {
        "type": "object",
        "properties": {
          "username": {
            "type": "string",
            "description": "Request Json Propert for UserName",
            "nullable": true
          },
          "password": {
            "type": "string",
            "description": "Request Json Propert for Password",
            "nullable": true
          },
          "email": {
            "type": "string",
            "description": "Request Json Propert for Email",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "AddCountryRequest": {
        "type": "object",
        "properties": {
          "countryName": {
            "type": "string",
            "description": "Request Json Propert for Country Name",
            "nullable": true
          },
          "city": {
            "type": "string",
            "description": "Request Json Property for City",
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "User": {
        "type": "object",
        "properties": {
          "id": {
            "type": "integer",
            "description": "Response UserID where it will created while inserting into DB. This is used for reterving the users Country and City",
            "format": "int32"
          },
          "name": {
            "type": "string",
            "description": "Response UserName",
            "nullable": true
          },
          "email": {
            "type": "string",
            "description": "Response Email for Authenticating the user",
            "nullable": true
          },
          "password": {
            "type": "string",
            "description": "Response Password for Authenticating the user",
            "nullable": true
          }
        },
        "additionalProperties": false
      }
    }
  }
}