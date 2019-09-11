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

    public function _testShouldEventsBeSorted()
    {
        $tmpFile = tmpfile();
        $fileName = stream_get_meta_data($tmpFile)['uri'];
        file_put_contents($fileName, '{}');

        $eventStore = new \App\EventStore\JsonFileSystemEventStore($fileName);
        $eventsToAppend = [
            new Event(['order' => 1]),
            new Event(['order' => 2]),
            new Event(['order' => 3]),
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
