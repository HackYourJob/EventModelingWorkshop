<?php


namespace App\Tests;
use App\Events\RoomAddedToInventory;
use App\EventStore\InMemoryEventStore;
use App\ReadModels\RoomExists;
use DateTimeImmutable;
use PHPUnit\Framework\TestCase;


class RoomExistsTest extends TestCase
{
    public function testShouldFindRoomExists()
    {
        $eventStore = new InMemoryEventStore();

        $roomExists = new RoomExists($eventStore);

        $this->assertFalse($roomExists(1));

        $eventStore->append(new RoomAddedToInventory(1, "double", new DateTimeImmutable()), "room1");

        $this->assertTrue($roomExists(1));
    }
}
