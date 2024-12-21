Feature: Withdrawals

  Scenario: Successful withdrawal
    Given the wallet balance is initialized to '10'
    When I withdraw funds from the wallet with amount '10'
    And I retrieve the balance
    Then the balance should be '0'

  Scenario: Withdrawal with insufficient funds
    Given the wallet balance is initialized to '0'
    When I withdraw funds from the wallet with amount '10'
    Then the balance should be '0'
    And the correct 'Invalid withdrawal amount. There are insufficient funds.' error is shown with status '400'

  Scenario: Withdrawal with invalid amount
    Given the wallet balance is initialized to '0'
    When I withdraw funds from the wallet with amount 'abc'
    Then the balance should be '0'
    And the correct 'One or more validation errors occurred.' error is shown with status '400'