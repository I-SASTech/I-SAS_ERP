<template>
    <div>
        <button @click="SyncData">Sync</button>
        <button @click="PullData">Pull Sync</button>
        <button class="btn btn-primary  " v-on:click="syncSetup()">Sync Setup</button>

    </div>
</template>
<script>
    export default {
        name: 'sync-data',
        data: function () {
            return {
                cvb:''
            }
            },
        methods: {
                        syncSetup: function () {
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.$https.get('/System/SyncSetup', { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        console.log(response.data);
                    })
            },
            SyncData: function () {
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }


                this.$https
                    .get('/System/PushDataRecord', { headers: { "Authorization": `Bearer ${token}` } });
            },

             PullData: function () {
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }


                this.$https
                    .get('/System/PullData', { headers: { "Authorization": `Bearer ${token}` } });
            }
        }
        }


</script>
