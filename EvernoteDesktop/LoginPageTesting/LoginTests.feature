Feature: LoginPageTesting
	Testing the login features

Scenario: Login Valid
	Given I am on login page
	And I enter login details
	When I click submit
	Then I am logged in