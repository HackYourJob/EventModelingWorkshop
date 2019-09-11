<?php


namespace App\Controller;


use App\Events\RoomTypePriceChanged;
use App\EventStore\EventStore;
use DateTimeImmutable;
use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\HttpFoundation\Response;

class ChangeRoomTypePriceController extends AbstractController
{
    /**
     * @var EventStore
     */
    private $eventStore;

    public function __construct(EventStore $eventStore)
    {
        $this->eventStore = $eventStore;
    }

    public function __invoke(Request $request): Response
    {
        $type = $request->request->get('type');
        $price = $request->request->getInt('price');

        $this->eventStore->append(new RoomTypePriceChanged($type, $price, new DateTimeImmutable()));

        return $this->redirectToRoute('room_types_dashboard');
    }
}
