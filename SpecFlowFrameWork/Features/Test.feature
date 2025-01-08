@BeforeFeature
Feature: Test

A short summary of the feature

@Test
Scenario: Test
    Given I have entered a valid username and password
    When I click the login button
    Then I should be redirected to the dashboard