<?php


namespace App\Tests;
use App\Events\RoomAddedToInventory;
use App\EventStore\InMemoryEventStore;
use App\ReadModels\RoomExists;
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
        $inventory = $roomInventory();
        $this->assertCount(1, $inventory);

        $eventStore->append(new RoomAddedToInventory(2, "twin", new DateTimeImmutable()), "room2");
        $inventory = $roomInventory();
        $this->assertCount(2, $inventory);
    }
}
