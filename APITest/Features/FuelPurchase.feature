Feature: Fuel Purchase API Automation

  Background:
    Given the test data is reset

  Scenario Outline: Buy a quantity of each fuel
    When I buy <Qty> units of "<FuelType>"
    Then I should count the orders which were placed before system current date
    Examples: 
    | Qty | FuelType |
    | 10  | Elec   |
    | 15  | Oil   |
