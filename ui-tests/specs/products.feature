Feature: Product Page
  As a user, I want to be able to see the correct products listed with accurate prices and descriptions when I choose a category, so that I can easily filter and view product details.

  Scenario Outline: Check that the category <category> shows the product <product> with correct price and description.
    Given that I am on the product page
    When I choose the category "<category>"
    Then I should see the product "<product>" with price "<price>" and description "<description>"

    Examples:
      | category | product                    | price | description                                                   |
      | Godis    | Mormors chokladbollar      | 3     | Mormors äkta chokladbollar, hemmabakade.                      |
      | Godis    | Farbror Konrads kåldolmar  | 10    | Läckra sockrade kåldolmar, från Konrads Bageri.               |
      | Godis    | Dans Dansande Degknyten    | 9     | Degknyten som verkligen får magen att dansa, på nolltid!      |
      | Läsk     | Bennys Bengaliska Bubblor  | 15    | Bubbligare läsk går ej att hitta. Dricks på egen risk.        |
      | Läsk     | Klas Kluriga Kombuchaläsk  | 8     | Kombucha så klurig att dess existens inte går att förstå.     |
      | Läsk     | Erics Erfarna Ekoläsk      | 2     | Halvt äkta läsk lagrad på oljefat                             |
      | Tobak    | Ettan lös                  | 45    | Lössnus från märket Ettan.                                    |
      | Tobak    | Velo Mint Xtreme-strong    | 55    | Velos dundersnus. Deras starkaste variant.                    |
      | Tobak    | Siberia Portion            | 70    | Portionssnus från märket Siberia. 1000 mg nikotin per prilla. |
      | Avfall   | Menaids Magiska Multidryck | 5     | Menaids magiskt goda multidryck, bryggd i Åstorp.             |
      | Avfall   | Blandad kompost.           | 1     | Ruttet kompostavfall.                                         |
      | Avfall   | Miljöfarligt avfall.       | 99    | Restprodukter och biprodukter av blandat slag.                |
      | Choklad  | Marabou Stenbitsrom        | 12    | Läckerheter från havet möter mmm marabou.                     |
      | Choklad  | Marabou Jul                | 12    | Choklad med smak av julen.                                    |
      | Choklad  | Marabou Lök                | 12    | Lökig choklad.                                                |

  Scenario Outline: Check that the category <category> does not show the product <product>.
    Given that I am on the product page
    When I choose the category "<category>"
    Then I should not see the product "<product>"

    Examples:
      | category | product                   |
      | Godis    | Ettan lös                 |
      | Godis    | Bennys Bengaliska Bubblor |
      | Läsk     | Mormors chokladbollar     |
      | Läsk     | Ettan lös                 |
      | Tobak    | Mormors chokladbollar     |
      | Tobak    | Bennys Bengaliska Bubblor |
      | Avfall   | Ettan lös                 |
      | Avfall   | Mormors chokladbollar     |
      | Choklad  | Ettan lös                 |
      | Choklad  | Bennys Bengaliska Bubblor |

  Scenario: Check that all products are displayed when "Alla" is selected.
    Given that I am on the product page
    When I choose the category "Alla"
    Then I should see all products with correct prices and descriptions
