Feature: Login API Automation

  Scenario Outline: Perfom login with valid credentials
    When I login with "<Username>" and "<Password>"
    Then I should get valid access token
    Examples: 
    | Username | Password |
    | test  | testing   |
