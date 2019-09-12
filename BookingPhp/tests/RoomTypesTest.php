<?php


namespace App\Tests;
use App\Events\RoomTypePriceChanged;
use App\EventStore\InMemoryEventStore;
use App\ReadModels\RoomTypes;
use DateTimeImmutable;
use PHPUnit\Framework\TestCase;


class RoomTypesTest extends TestCase
{
    public function testShouldFindRoomExists()
    {
        $eventStore = new InMemoryEventStore();

        $roomTypes = new RoomTypes($eventStore);

        $this->assertEquals([
            'twin' => [
                'type' => 'twin',
                'price' => 90,
            ],
            'double' => [
                'type' => 'double',
                'price' => 100,
            ],
        ], $roomTypes());

        $eventStore->append(new RoomTypePriceChanged("double", 99, new DateTimeImmutable()), "double");

        $this->assertEquals([
            'twin' => [
                'type' => 'twin',
                'price' => 90,
            ],
            'double' => [
                'type' => 'double',
                'price' => 99,
            ],
        ], $roomTypes());

        $eventStore->append(new RoomTypePriceChanged("twin", 50, new DateTimeImmutable()), "twin");

        $this->assertEquals([
            'twin' => [
                'type' => 'twin',
                'price' => 50,
            ],
            'double' => [
                'type' => 'double',
                'price' => 99,
            ],
        ], $roomTypes());
    }
}
