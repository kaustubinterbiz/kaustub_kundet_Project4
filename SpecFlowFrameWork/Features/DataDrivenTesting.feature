@BeforeFeature
Feature: DataDrivenTesting

A short summary of the feature

@DataDriven
Scenario Outline: DataDrivenTesting
    Given  Entered a valid "<Username>" and "<Password>"
    When I click the login button
    #Then Validation of Login Credentials and User Access
    Then Validate Login Message After Clicking the Login Button
    Then Validation of Login Credentials "<Username>" and "<Password>"
    
    Examples: 
     | Username | Password             |
     | practice1 | SuperSecretPassword1 |
     | practice2 | SuperSecretPassword2 |
     | practice3 | SuperSecretPassword3 |
     | practice | SuperSecretPassword! |

@DataTakenFromJson
Scenario: Data Taken from Json File
   Given Enter the Credentials
   When I click the login button
   Then I should be redirected to the dashboard
