<template>
    <div style="margin-bottom:0px" class="card">
            <div class="card-body">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        <div class="modal-header">
                            <h5 class="modal-title">Fetch Computer Bios Detials</h5>
                        </div>
                        <div class="text-left">
                            <div class="card-body">
                                <div class="row ">
                                    <button type="button" class="btn btn-primary " v-on:click="SearchDetails">Search Detials</button>
                                </div>
                                <div class="row ">
                                    <span class="text-danger">
                                        {{details}}
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</template>

<script>
export default {
    data: function() {
        return{
            details: ''
        }
    },
    methods: {
        SearchDetails: function (){
            fetch('https://jsonip.com/')
            .then(response => response.json())
            .then(response => {
                this.details = response.ip;
            });
        }
    }
}
</script>