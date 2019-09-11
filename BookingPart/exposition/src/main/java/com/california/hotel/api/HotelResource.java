package com.california.hotel.api;


import io.swagger.annotations.ApiOperation;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(value="/api")
public class HotelResource {

    public HotelResource() {}

    @ApiOperation(value="A simple test", notes="A simple test before starting the real deal")
    @GetMapping(value={"/hotel"}, produces=MediaType.APPLICATION_JSON_VALUE)
    public ResponseEntity<Integer> booksHotelRoom() {
        return new ResponseEntity<>(Integer.valueOf(1), HttpStatus.OK);
    }
}