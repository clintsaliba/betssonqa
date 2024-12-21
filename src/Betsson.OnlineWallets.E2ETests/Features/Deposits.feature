Feature: Deposits

  Scenario: Retrieve balance when there are no transactions

    Given the wallet balance is initialized to 0
    When I retrieve the balance
    Then the balance should be 0

