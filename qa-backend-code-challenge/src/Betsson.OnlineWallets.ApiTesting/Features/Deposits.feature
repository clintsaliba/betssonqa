Feature: Deposit 

  Background:
    Given the wallet balance is initialized to '0'

  Scenario: Successful deposit
    When I deposit funds in the wallet with amount '10'
    And I retrieve the balance
    Then the balance should be '10'

  Scenario Outline: Deposit with invalid amount
    When I deposit funds in the wallet with amount '<deposit_amount>'
    Then the balance should be '<expected_balance>'
    And the correct '<error_message>' error is shown with status '<status_code>'

    Examples:
      | deposit_amount | expected_balance | error_message                            | status_code |
      | abc            | 0                | One or more validation errors occurred.  | 400         |
      | -10            | 0                | One or more validation errors occurred.  | 400         |
    