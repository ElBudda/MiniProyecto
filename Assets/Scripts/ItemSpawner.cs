x+)JMU04�d040031Q��O�w��+)v/-)I-J�/���HM-�+�Kg�y���8�*(��a��~���fw]!F�^njI"æ�?�T�o�����J$����� �n1y                                                                                                                                                                                                                                                                                                                                                                                                                        nds(spawnInterval);

            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject randomItem = itemsToSpawn[Random.Range(0, itemsToSpawn.Length)];

            Instantiate(randomItem, randomSpawnPoint.position, Quaternion.identity);
        }
    }
}

