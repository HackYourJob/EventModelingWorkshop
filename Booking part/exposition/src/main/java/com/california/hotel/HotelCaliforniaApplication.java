package com.california.hotel;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.ComponentScan;

@SpringBootApplication
@ComponentScan(basePackages = {"com.california.hotel"})
public class HotelCaliforniaApplication {
	public static void main(String[] args) {
		SpringApplication.run(HotelCaliforniaApplication.class, args);
	}
}
