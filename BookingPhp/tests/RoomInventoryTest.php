<?php


namespace App\Tests;
use App\Events\RoomAddedToInventory;
use App\EventStore\InMemoryEventStore;
use App\ReadModels\RoomInventory;
use DateTimeImmutable;
use PHPUnit\Framework\TestCase;


class RoomInventoryTest extends TestCase
{
    public function testShouldFindRoomExists()
    {
        $eventStore = new InMemoryEventStore();

        $roomInventory = new RoomInventory($eventStore);

        $this->assertEmpty($roomInventory());

        $eventStore->append(new RoomAddedToInventory(1, "double", new DateTimeImmutable()), "room1");
        $this->assertEquals([
            [
                'id' => 1,
                'type' => 'double',
            ],
        ], $roomInventory());

        $eventStore->append(new RoomAddedToInventory(2, "twin", new DateTimeImmutable()), "room2");
        $this->assertEquals([
            [
                'id' => 1,
                'type' => 'double',
            ],
            [
                'id' => 2,
                'type' => 'twin',
            ],
        ], $roomInventory());
    }
}
