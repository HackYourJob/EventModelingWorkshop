package com.heiwait.tripagency.domain.cucumber;

import cucumber.api.CucumberOptions;
import cucumber.api.junit.Cucumber;
import org.junit.runner.RunWith;

/**
 * Created by Dan on 25/06/2017.
 */
@RunWith(Cucumber.class)
@CucumberOptions(
        features = "src/test/cucumber"
        , glue = {"com.california.hotel"}
        , plugin = {"json:target/cucumber/HotelCalifornia.json", "html:target/cucumber/HotelCalifornia.html", "pretty"}
)
public class BDDRunner {
}
