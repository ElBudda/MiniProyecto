x+)JMU03�`040031QpJL�N/�/�Kq��+)���I-�K.f��{u�ٷ�ɦ�%d���=~x����ԒD��@�0ֻ��F�\���i�����%��/3=�$�dõ�̿^�y�l^l��,����U��B�dZ1eNL��Ϯ^_z@С'�ڳ$57� �<�p�m�)J���Z���v��~|�J��|�&˿��=��[{�ꟿÙ3A�����ꓟ��r���닱�DU>���u?}CA/.���v��-ix>��1�WhoN�3���Ē��̂�b�R��gJG��oo���Xh2c���,&@��Pɰ��B�$)�-=�����~�t�b�5/8�<���SG�Ę�{|޽���I��㡂�W�)�ؽCѡ��*z�S\}��~u;f�m��)MJH,� �߹p��)�*oj��/_�lb���H� F���Z�as|��K����pL7� .��                                                    ount;
    }

    public void RemoveItem(string itemName, int amount)
    {
        if (HasItem(itemName, amount))
        {
            items[itemName] -= amount;
            if (items[itemName] <= 0)
                items.Remove(itemName);
        }
    }
}

