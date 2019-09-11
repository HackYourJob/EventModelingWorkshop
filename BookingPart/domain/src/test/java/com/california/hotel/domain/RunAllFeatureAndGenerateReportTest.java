package com.california.hotel.domain;


import com.california.hotel.domain.cucumber.BDDRunner;
import org.junit.runner.RunWith;
import org.junit.runners.Suite;

@RunWith(Suite.class)
@Suite.SuiteClasses({BDDRunner.class})
public class RunAllFeatureAndGenerateReportTest {
}
