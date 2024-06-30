<template>
    <modal show="show" :modalLarge="true">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel">View Document</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body text-center">
                <iframe :key="changereport" height="1550" width="1000" :src="reportsrc"></iframe>
            </div>
            
        </div>
    
    </modal>
    </template>
    
    <script>
   
    export default {
        name: 'InvoiceDetailsPrint',
        props: ['show', 'changereport', 'reportsrc'],
        
        data: function () {
            return {
    
            }
        },
        
        methods: {
            close: function () {
                
    
                this.$emit('close');
            },
    
            
        },
        mounted: function () {
            
           
        }
    }
    </script>
    