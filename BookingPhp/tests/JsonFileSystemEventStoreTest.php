<?php

namespace App\Tests;

use App\EventStore\Event;
use PHPUnit\Framework\TestCase;

class JsonFileSystemEventStoreTest extends TestCase
{
    public function tearDown()
    {
        parent::tearDown();

        foreach(glob(__DIR__ . '/fixtures/AppendableStore/*.json') as $file) {
            unlink($file);
        }
    }

    public function testEventStoreCreatedFromEmptyDirectoryIsEmpty()
    {
        $eventStore = new \App\EventStore\JsonFileSystemEventStore(__DIR__ . '/fixtures/EmptyStore');

        $this->assertEmpty(
            $eventStore->getEvents()
        );

    }

    public function testShouldAppendEventToStore()
    {
        $dir = __DIR__ . '/fixtures/AppendableStore';
        $eventStore = new \App\EventStore\JsonFileSystemEventStore($dir);
        $eventToAppend = new Event(
            'room',
            ['roomId' => 404],
            new \DateTimeImmutable()
        );

        $eventStore->append($eventToAppend);
        $allEvents = $eventStore->getEvents();

        $this->assertEquals(
            [
                $eventToAppend
            ],
            $allEvents
        );
    }

    public function testShouldEventsBeSorted()
    {
        $dir = __DIR__ . '/fixtures/AppendableStore';
        $eventStore = new \App\EventStore\JsonFileSystemEventStore($dir);
        $eventsToAppend = [
            new Event(
                'roomAdded',
                    [
                    'id' => 1,
                    'type' => 'double',
                    ],
                $date = new \DateTimeImmutable()
            ),
            new Event(
                'roomAdded',
                [
                    'id' => 2,
                    'type' => 'double',
                ],
                $date = $date->add(new \DateInterval('P10D'))
            ),
            new Event(
                'roomAdded',
                [
                    'id' => 3,
                    'type' => 'king',
                ],
                $date = $date->add(new \DateInterval('P10D'))
            ),
        ];

        foreach ($eventsToAppend as $event) {
            $eventStore->append($event);
        }
        $allEvents = $eventStore->getEvents();

        $this->assertEquals(
            $eventsToAppend,
            $allEvents
        );
    }

}
