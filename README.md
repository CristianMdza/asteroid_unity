# asteroid_unity

Éviter les asteroïdes ! La scène principale : `Scenes` > `Asteroid Dodger`.

## Notes de développement

- La méthode `SetShipY(targetY)` du script `AsteroidDodger` permet de déplacer le vaisseau (pour être possiblement contrôlé par un potentiomètre, une photorésistance ou une manette).
- La méthode `GetShipIsVisible()` du script `AsteroidDodger` permet de savoir si le vaisseau est visible (utile pour synchroniser le clignotement d'une DEL avec le vaisseau par exemple).
